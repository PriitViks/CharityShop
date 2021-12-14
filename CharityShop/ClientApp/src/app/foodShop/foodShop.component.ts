import { Component, Inject } from '@angular/core';
import { BasketService } from '../services/basket.service';
import { ShopItemDto } from '../interfaces/shopItemDto';
import { FormBuilder } from '@angular/forms';
import { CommunicationService } from '../services/communication.service';

@Component({
  selector: 'app-foodShop',
  templateUrl: './foodShop.component.html'
})

export class FoodShopComponent {
  public shopItems: ShopItemDto[] = [];
  moneyToReturnCustomer: number = 0;
  customerPaidAmount: number = 0;
  returnedCurrency: string = '';
  itemsInBasket = this.basketService.itemsInBasket;
  checkoutForm = this.formBuilder.group({ paid: '' });

  constructor(
    private basketService: BasketService,
    private formBuilder: FormBuilder,
    private uploadService: CommunicationService
  ) {
    this.updateShopingItems();
  }

  updateShopingItems() {
    this.uploadService.getShopItemList().subscribe(
      result => {
        this.shopItems = result;
      }, error => console.error(error)
    );
  }

  public canBeRemoved(shopItem: ShopItemDto) {
    if (this.basketService.itemsInBasket.some(e => e.id == shopItem.id))
      return true;
    else
      return false;
  }

  public quantityInBasket(shopItem: ShopItemDto) {
    if (this.basketService.itemsInBasket.some(e => e.id == shopItem.id))
      return "Items in basket:" +
        this.basketService.itemsInBasket[this.basketService.itemsInBasket.findIndex(e => e.id == shopItem.id)].quantity;
    return null;
  }

  public addToBasket(addedItem: ShopItemDto) {
    this.uploadService.addToBasket(addedItem.id)
      .subscribe(
        result => {
          this.basketService.addItemToBasket(addedItem);
          this.updateShopingListQuantity(addedItem.id, result.quantity);
        },
        error => {
          if (error.Text == "Sold out") {
            this.updateShopingListQuantity(addedItem.id, 0);
            alert("Item is already sold out");
          }
          else
            console.log(error);
        });
  }

  private updateShopingListQuantity(messageID: number, updateQuantity: number) {
    this.shopItems[this.shopItems.findIndex(e => e.id == messageID)].quantity = updateQuantity;
  }

  public removeFromBasket(removedItem: ShopItemDto) {
    this.basketService.removeItemFromBasket(removedItem);
    const copyOfRemoved = { ...removedItem };
    copyOfRemoved.quantity = 1;

    this.uploadService.removeFromBasket(copyOfRemoved)
      .subscribe(
        result => { this.updateShopingListQuantity(removedItem.id, result.quantity); },
        error => { console.log(error); });
  }

  public getOrderTotalSum() {
    var total = 0;
    for (var i = 0; i < this.basketService.itemsInBasket.length; i++) {
      total += this.basketService.itemsInBasket[i].quantity * this.basketService.itemsInBasket[i].price;
    }
    return total;
  }

 
  public customerPaid() {
    this.returnedCurrency = '';
    var sumPaid = this.customerPaidAmount;
    var totalSum = this.getOrderTotalSum();
    this.moneyToReturnCustomer = parseFloat((sumPaid - totalSum).toFixed(2));
    if (sumPaid == 0 || sumPaid < totalSum) {
      alert("Sum paid is less then basket value");
    }
    else {
      this.uploadService.moneyToReturn(this.moneyToReturnCustomer)
        .subscribe(
          result => {
            for (var i = 0; i < result.length; i++) {
              this.returnedCurrency += result[i].quantity + " time " + result[i].name + "\n";             
            }           
            this.itemsInBasket = this.basketService.clearBasket();
          },
          error => { console.log(error); });
    }
  }

  public cancelOrder() {
    for (var i = 0; i < this.basketService.itemsInBasket.length; i++) {
      var itemToRemove = this.basketService.itemsInBasket[i];
      this.uploadService.removeFromBasket(itemToRemove)
        .subscribe(
          result => {
            var index = this.basketService.itemsInBasket.findIndex(e => e.id == result.id);
            this.basketService.itemsInBasket.splice(index, 1)
          },
          error => { console.log(error); });
    }
  }  
}

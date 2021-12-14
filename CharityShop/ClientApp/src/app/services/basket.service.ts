import { ShopItemDto } from '../interfaces/shopItemDto';

export class BasketService {
  public itemsInBasket: ShopItemDto[] = [];

  addItemToBasket(addedItem: ShopItemDto) {
    if (this.itemsInBasket.some(e => e.id == addedItem.id)) {
      this.itemsInBasket[this.itemsInBasket.findIndex(e => e.id == addedItem.id)].quantity += 1;
    }
    else {
      var itemToAdd = { ...addedItem };
      itemToAdd.quantity = 1;
      this.itemsInBasket.push(itemToAdd);
    }
  }

  removeItemFromBasket(removeItem: ShopItemDto) {
    var indexOfItemInBasket = this.itemsInBasket.findIndex(e => e.id == removeItem.id);
    var itemInBasket = this.itemsInBasket[indexOfItemInBasket];

    if (itemInBasket.quantity > 1) {
      itemInBasket.quantity -= 1;
    }
    else {
      this.itemsInBasket.splice(indexOfItemInBasket, 1);
    }
  }
  clearBasket() {
    this.itemsInBasket = [];
    return this.itemsInBasket;
  }
}

import { Component, Inject } from '@angular/core';

import { ShopItemDto } from '../interfaces/shopItemDto';
import { CommunicationService } from '../services/communication.service';

@Component({
  selector: 'app-updateStock',
  templateUrl: './updateStock.component.html'
})
export class UpdateStockComponent {
  public shopItems: ShopItemDto[] = [];
  chosenFileName: any;
  chosenFile: any;

  constructor(private uploadService: CommunicationService) {
    this.updateList();
  }

  updateList() {
    this.uploadService.getShopItemList().subscribe(
      result => {
        this.shopItems = result;
      }, error => console.error(error)
    );
  }

  updateItems(itemUpdateQuantity: ShopItemDto) {
    this.uploadService.removeFromBasket(itemUpdateQuantity)
      .subscribe(
        result => {
          alert("Item has been updated")
        },
        error => { console.log(error); });
  }

  setChosenFile(fileInput: Event) {
    const control: any = fileInput.target;
    if (!control.files || control.length === 0) {
      this.chosenFileName = null;
      this.chosenFile = null;
    }
    else {
      this.chosenFileName = control.files[0].name;
      this.chosenFile = control.files[0];
    }
  }

  uploadFile() {
    const uploadData = new FormData();
    uploadData.append('file', this.chosenFile, this.chosenFileName);
    this.uploadService
      .uploadFile(uploadData)
      .subscribe(
        result => {},
        error => {
          if (error.status == 200) {
            alert('File uploaded successfully');
            this.updateList();
          }
          else {
            alert('Failed to uploaded document');
          }

        }
      );
  }

}



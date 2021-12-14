import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ShopItemDto } from '../interfaces/shopItemDto';
import { Money } from '../interfaces/money';

@Injectable()
export class CommunicationService {

  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) { }

  public uploadFile(data: any) {
    return this.http.post<string>(this.baseUrl + 'shopitem', data);
  }

  public getShopItemList() {
    return this.http.get<any[]>(this.baseUrl + 'shopitem');
  }

  public addToBasket(id: number) {
    return this.http.put<ShopItemDto>(this.baseUrl + 'shopitem/itemsold', id)
  }

  public removeFromBasket(removedItem: ShopItemDto) {
    return this.http.put<ShopItemDto>(this.baseUrl + 'shopitem/ItemReturned', removedItem)
  }

  public moneyToReturn(returnedAmount: number) {
    return this.http.put<Money[]>(this.baseUrl + 'shopitem/MoneyToReturn', returnedAmount);
  }
}



import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { UpdateStockComponent } from './updateStock/updateStock.component';
import { FoodShopComponent } from './foodShop/foodShop.component';
import { BasketService } from './services/basket.service';
import { CommunicationService } from './services/communication.service';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTableModule } from '@angular/material/table';
import { FlexLayoutModule } from '@angular/flex-layout';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    UpdateStockComponent,
    FoodShopComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule,
    MatFormFieldModule,
    MatTableModule,
    FlexLayoutModule,
    BrowserAnimationsModule,
    RouterModule.forRoot([
      { path: '', component: FoodShopComponent, pathMatch: 'full' },
      { path: 'updateStock', component: UpdateStockComponent },
    ])
  ],
  providers: [
    BasketService,
    CommunicationService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

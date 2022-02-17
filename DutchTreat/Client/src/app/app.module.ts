import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { Store } from './services/store.service';
import ProductListView from './views/productListView.component';
import { CartView } from './views/cartView.component';
import router from './router';
import { ShopPage } from './pages/shopPage.component';
import { Checkout } from './pages/checkout.component';
import { LoginPage } from './pages/loginPage.component';
import { AuthActivator } from './services/authActivator.service';

@NgModule({
    declarations: [
        AppComponent,
        ProductListView,
        CartView,
        ShopPage,
        Checkout,
        LoginPage
    ],
    imports: [
        BrowserModule,
        HttpClientModule,
        router
    ],
    providers: [
        Store,
        AuthActivator
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }

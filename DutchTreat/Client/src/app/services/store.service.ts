import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map, Observable } from "rxjs";
import { IOrder } from "../interfaces/order.interface";
import { IProduct } from "../interfaces/product.interface";
import { LoginRequest, LoginResults } from "../shared/LoginResults";
import { Order, OrderItem } from "../shared/Order";

@Injectable()
export class Store {

    public products: IProduct[];
    public order: IOrder;
    public token = "";
    public expiration = new Date();

    constructor(private http: HttpClient) {
        this.products = [];
        this.order = new Order();
    }

    loadProducts() {
        return this.http.get<IProduct[]>("/api/products")
            .pipe(map(data => {
                this.products = data;
            }));
    }

    get loginRequired(): boolean {
        return this.token.length === 0 || this.expiration < new Date();
    }

    login(creds: LoginRequest) {
        return this.http.post<LoginResults>("/account/createtoken", creds)
            .pipe(map(data => {
                this.token = data.token;
                this.expiration = data.expiration;
            }));
    }

    addToOrder(product: IProduct) {

        let item = this.order.items.find(o => o.productId === product.id);

        if (item) {
            item.quantity++;
        }
        else {
            item = new OrderItem();
            item.id = 0;
            item.quantity = 1;
            item.productId = product.id;
            item.productTitle = product.title;
            item.productArtId = product.artId;
            item.productArtist = product.artist;
            item.productCategory = product.category;
            item.productSize = product.size;
            item.unitPrice = product.price;

            this.order?.items.push(item);
        }
    }
}
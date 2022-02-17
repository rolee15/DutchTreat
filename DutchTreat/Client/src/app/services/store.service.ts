import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map, Observable } from "rxjs";
import { IOrder } from "../interfaces/order.interface";
import { IProduct } from "../interfaces/product.interface";
import { Order } from "../shared/Order";

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

    loadProducts(): Observable<void> {
        return this.http.get<IProduct[]>("/api/products")
            .pipe(map(data => {
                this.products = data;
                return;
            }));
    }

    get loginRequired(): boolean {
        return this.token.length === 0 || this.expiration < new Date();
    }

    addToOrder(product: IProduct) {

        let item = this.order.items.find(o => o.productId === product.id);

        if (item) {
            item.quantity++;
        }
        else {
            item = {
                id: 0,
                quantity: 1,
                productId: product.id,
                productTitle: product.title,
                productArtId: product.artId,
                productArtist: product.artist,
                productCategory: product.category,
                productSize: product.size,
                unitPrice: product.price
            };
            this.order?.items.push(item);
        }
    }
}
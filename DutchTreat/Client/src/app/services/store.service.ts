import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map, Observable } from "rxjs";
import { Order, OrderItem } from "../interfaces/order.interface";
import { Product } from "../interfaces/product.interface";

@Injectable()
export class Store {

  public products: Product[] = [];

  public order: Order = {
    orderId: 0,
    orderNumber: "0",
    orderDate: new Date(),
    items: []
  };

  constructor(private http: HttpClient) {
  }

  loadProducts(): Observable<void> {
    return this.http.get<Product[]>("/api/products")
      .pipe(map(data => {
        this.products = data;
        return;
      }));
  }

  addToOrder(product: Product) {

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

  subtotal(): number {
    const result = this.order.items.reduce((total, value) => {
      return total + (value.unitPrice * value.quantity);
    }, 0);

    return result;
  }
}
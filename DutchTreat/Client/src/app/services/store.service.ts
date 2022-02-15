import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map, Observable } from "rxjs";
import { Product } from "../interfaces/product.interface";

@Injectable()
export class Store {

  public products: Product[] = [];

  constructor(private http: HttpClient) {

  }

  loadProducts(): Observable<void> {
    return this.http.get<Product[]>("/api/products")
      .pipe(map(data => {
        this.products = data;
        return;
      }));
  }
}
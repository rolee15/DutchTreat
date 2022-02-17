import { IOrder, IOrderItem } from "../interfaces/order.interface";

export class Order implements IOrder {
    orderId: number;
    orderDate: Date;
    orderNumber: string;
    items: IOrderItem[];

    constructor() {
        this.orderId = 0
        this.orderDate = new Date();
        this.orderNumber = "";
        this.items = [];
    }

    public get subtotal(): number {
        return this.items.reduce((total, value) => {
            return total + (value.unitPrice * value.quantity);
        }, 0);
    }

}

export class OrderItem implements IOrderItem {
    id: number;
    quantity: number;
    unitPrice: number;
    productId: number;
    productCategory: string;
    productSize: string;
    productTitle: string;
    productArtist: string;
    productArtId: string;

    constructor() {
        this.id = 0;
        this.quantity = 0;
        this.unitPrice = 0;
        this.productId = 0;
        this.productCategory = "";
        this.productSize = "";
        this.productTitle = "";
        this.productArtist = "";
        this.productArtId = "";

    }
}
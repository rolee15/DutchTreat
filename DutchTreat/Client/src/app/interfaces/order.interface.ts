export interface IOrder {
    orderId: number;
    orderDate: Date;
    orderNumber: string;
    items: IOrderItem[];

    get subtotal(): number;
}

export interface IOrderItem {
    id: number;
    quantity: number;
    unitPrice: number;
    productId: number;
    productCategory: string;
    productSize: string;
    productTitle: string;
    productArtist: string;
    productArtId: string;
}
import {IOrderItem} from './orderItem.model';

export interface IOrderDetail {
    ordernumber: string;
    status: string;
    description: string;
    street: string;
    date: Date;
    city: number;
    state: string;
    zipcode: string;
    country: number;
    total: number;
    orderitems: IOrderItem[];
    subtotal: number;
    discount: number;
    discountCode: string;
    points: number;
}

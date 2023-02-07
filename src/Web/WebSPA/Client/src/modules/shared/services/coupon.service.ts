import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICoupon } from '../models/coupon.model';
import { ConfigurationService } from './configuration.service';
import { DataService } from './data.service';
import { tap } from 'rxjs/operators';
import { ILoyalty } from '../models/loyalty.model';

@Injectable({ providedIn: 'root' })
export class CouponService {
    constructor(private service: DataService, private configurationService: ConfigurationService) {
        if (this.configurationService.isReady)
            this.couponUrl = this.configurationService.serverSettings.purchaseUrl;
        else
            this.configurationService.settingsLoaded$.subscribe(x => this.couponUrl = this.configurationService.serverSettings.purchaseUrl);
    }

    couponUrl: string

    getCoupon(code: string): Observable<ICoupon> {
        const path = `/cp/api/coupons/${code}`;
        return this.service.get(`${this.couponUrl}${path}`).pipe<ICoupon>(
            tap((response: any) => {
                if (response.status == 404) {
                    return null;
                }

                return response;
            })
        );
    }

    getPoints(): Observable<number> {
        const path = '/l/api/loyalty';
        const coupon = this.couponUrl;
        return this.service.get(`${coupon}${path}`).pipe<number>(
            tap((response: any) => {
                if (response.status == 404) {
                    return 0;
                }

                return response;
            })
        );
    }
}
import { Component, OnInit } from '@angular/core';
import { CouponService } from '../shared/services/coupon.service';
import { SecurityService } from '../shared/services/security.service';

@Component({
    selector: 'profile',
    templateUrl: './profile.component.html'
})
export class ProfileComponent implements OnInit {
    points: number = 0;
    name: string;
    constructor(private service: SecurityService, private couponService: CouponService) { }
    ngOnInit(): void {

        if (this.service.IsAuthorized) {
            this.name = this.service.UserData.name;
            this.couponService.getPoints().subscribe(resp => {
                this.applyPoints(resp);
            });
        }
    }

    applyPoints(amount: number) {
        this.points = amount;
    }
}
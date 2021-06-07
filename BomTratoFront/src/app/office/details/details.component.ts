import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { Office } from '../../shared/models/office';
import { OfficeService } from '../../shared/services/office.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html'
})
export class DetailsComponent{

  office: Office = new Office();
  date: Date;
  addressToMap;

  constructor(
    private route: ActivatedRoute,
    private aproverService: OfficeService,
    private sanitizer: DomSanitizer) {

    this.office = this.route.snapshot.data['aprover'];
    this.addressToMap = this.sanitizer.bypassSecurityTrustResourceUrl("https://www.google.com/maps/embed/v1/place?q=" + this.fullAddress() + "&key=AIzaSyAP0WKpL7uTRHGKWyakgQXbW6FUhrrA5pE");


  }

  public fullAddress(): string {
    return this.office.street + ", " + this.office.number + " - " + this.office.district + ", " + this.office.city + " - " + this.office.state;
  }


}

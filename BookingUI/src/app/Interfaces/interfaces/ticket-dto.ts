import { Data } from "@angular/router";

export interface TicketDto {
  id:number,
  userId:number,
  userName:string,
  tripId:number,
  fromCity:string,
  toCity:string,
  bookingDate:string,
  status:string
}

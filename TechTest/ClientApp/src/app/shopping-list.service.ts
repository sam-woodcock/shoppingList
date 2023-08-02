import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ShoppingListItem } from './shared/shopping-list-item.model';

@Injectable({
  providedIn: 'root',
})
export class ShoppingListService {
  private baseUrl = 'https://localhost:44318/api'; // Replace with your backend API URL

  constructor(private http: HttpClient) { }

  getShoppingList(): Observable<ShoppingListItem[]> {
    return this.http.get<ShoppingListItem[]>(`${this.baseUrl}/shoppinglist`);
  }

  addShoppingListItem(item: ShoppingListItem): Observable<ShoppingListItem> {
    return this.http.post<ShoppingListItem>(`${this.baseUrl}/shoppinglist`, item);
  }

  updateShoppingListItem(item: ShoppingListItem): Observable<ShoppingListItem> {
    return this.http.put<ShoppingListItem>(`${this.baseUrl}/shoppinglist/${item.id}`, item);
  }

  markAsBought(item: ShoppingListItem): Observable<ShoppingListItem> {
    // This method updates the isBought property to true
    item.isBought = true;
    return this.http.put<ShoppingListItem>(`${this.baseUrl}/shoppinglist/${item.id}/markAsBought`, item);
  }

  markAsNotBought(item: ShoppingListItem): Observable<ShoppingListItem> {
    // This method updates the isBought property to false
    item.isBought = false;
    return this.http.put<ShoppingListItem>(`${this.baseUrl}/shoppinglist/${item.id}/markAsNotBought`, item);
  }

  sortShoppingListAlphabetically(shoppingList: ShoppingListItem[]): void {
    shoppingList.sort((a, b) => a.name.localeCompare(b.name));
  }
}

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

  getShoppingList(): Observable<{ toBuyList: ShoppingListItem[], previouslyBoughtList: ShoppingListItem[] }> {
    return this.http.get<{ toBuyList: ShoppingListItem[], previouslyBoughtList: ShoppingListItem[] }>(`${this.baseUrl}/shoppinglist`);
  }

  addShoppingListItem(item: ShoppingListItem): Observable<ShoppingListItem> {
    return this.http.post<ShoppingListItem>(`${this.baseUrl}/shoppinglist`, item);
  }

  updateShoppingListItem(item: ShoppingListItem): Observable<ShoppingListItem> {
    return this.http.put<ShoppingListItem>(`${this.baseUrl}/shoppinglist/${item.id}`, item);
  }

  markAsBought(item: ShoppingListItem): Observable<ShoppingListItem> {
    // This method updates the isBought property to true
    return this.http.put<ShoppingListItem>(`${this.baseUrl}/shoppinglist/${item.id}/moveToPreviouslyBought`, item);
  }

  markAsNotBought(item: ShoppingListItem): Observable<ShoppingListItem> {
    // This method updates the isBought property to false

    return this.http.put<ShoppingListItem>(`${this.baseUrl}/shoppinglist/${item.id}/moveToBuy`, item);
  }

  copyToBuyListItem(item: ShoppingListItem): Observable<ShoppingListItem> {
    // If the item already exists in the To Buy list, increment its amount by 1
    return this.http.put<ShoppingListItem>(`${this.baseUrl}/shoppinglist/${item.id}/moveToBuy`, item);

  }
  deleteItem(item: ShoppingListItem): Observable<ShoppingListItem> {
    return this.http.put<ShoppingListItem>(`${this.baseUrl}/shoppinglist/${item.id}/delete`, item);

  }

  sortShoppingListAlphabetically(toBuyList: ShoppingListItem[], previouslyBoughtList: ShoppingListItem[]): void {
    toBuyList.sort((a, b) => a.name.localeCompare(b.name));
    previouslyBoughtList.sort((a, b) => a.name.localeCompare(b.name));
  }
}

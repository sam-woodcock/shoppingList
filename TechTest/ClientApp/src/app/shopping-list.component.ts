// shopping-list.component.ts
import { Component, OnInit } from '@angular/core';
import { ShoppingListItem } from './shared/shopping-list-item.model';
import { ShoppingListService } from './shopping-list.service';

@Component({
  selector: 'app-shopping-list',
  templateUrl: './shopping-list.component.html',
})
export class ShoppingListComponent implements OnInit {
  shoppingList: ShoppingListItem[] = [];
  previouslyBoughtList: ShoppingListItem[] = [];
  newItemName: string = '';

  constructor(private shoppingListService: ShoppingListService) { }

  ngOnInit() {
    this.loadShoppingList();
    this.loadPreviouslyBoughtList();
   
  }

  loadShoppingList() {
    this.shoppingListService.getShoppingList().subscribe((data) => {
      this.shoppingList = data.filter((item) => !item.isBought);
      this.shoppingListService.sortShoppingListAlphabetically(this.shoppingList);
    });
  }

  loadPreviouslyBoughtList() {
    this.shoppingListService.getShoppingList().subscribe((data) => {
      this.shoppingList = data.filter((item) => item.isBought);
      this.shoppingListService.sortShoppingListAlphabetically(this.shoppingList);
    });
  }

  addItem() {
    if (this.newItemName.trim() !== '') {
      const newItem: ShoppingListItem = {
        id: 0, // Assign a temporary ID (assuming you will get the real ID from the backend)
        name: this.newItemName.trim(),
        isBought: false, // New item is not bought by default
        isImportant: false, // New item is not important by default
      };

      this.shoppingListService.addShoppingListItem(newItem).subscribe((addedItem) => {
        this.shoppingList.push(addedItem);
        this.newItemName = ''; // Clear the input field after adding the item
      });
    }
  }

  markAsImportant(item: ShoppingListItem) {
    item.isImportant = true;
    this.shoppingListService.updateShoppingListItem(item).subscribe();
    this.sortItemsByImportance();
    this.loadShoppingList();
    this.loadPreviouslyBoughtList();
  }

  moveToPreviouslyBought(item: ShoppingListItem) {
    item.isBought = true;
    this.shoppingListService.updateShoppingListItem(item).subscribe((item)=> {
      this.loadShoppingList();
      this.loadPreviouslyBoughtList();
    });
  }

  moveToToBuy(item: ShoppingListItem) {
    item.isBought = false;
    this.shoppingListService.updateShoppingListItem(item).subscribe((item) => {
      this.loadShoppingList();
      this.loadPreviouslyBoughtList();
    });
  }

  private sortItemsByImportance() {
    this.shoppingList.sort((a, b) => {
      if (a.isImportant && !b.isImportant) {
        return -1;
      } else if (!a.isImportant && b.isImportant) {
        return 1;
      } else {
        return 0;
      }
    });
  }
}

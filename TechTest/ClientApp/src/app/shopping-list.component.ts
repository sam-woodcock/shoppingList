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
  }

  loadShoppingList() {
    this.shoppingListService.getShoppingList().subscribe((data) => {
      this.shoppingList = data.toBuyList;
      this.previouslyBoughtList = data.previouslyBoughtList;
      this.sortItemsByImportance();
    });
  }

  addItem() {
    if (this.newItemName.trim() !== '') {
      const newItem: ShoppingListItem = {
        id: 0, // Assign a temporary ID (assuming you will get the real ID from the backend)
        name: this.newItemName.trim(),
        isImportant: false, // New item is not important by default
        amount: 1,
      };

      this.shoppingListService.addShoppingListItem(newItem).subscribe(() => {
        this.loadShoppingList();
        this.newItemName = ''; // Clear the input field after adding the item
      });
    }
  }

  markAsImportant(item: ShoppingListItem) {
    item.isImportant = true;
    this.shoppingListService.updateShoppingListItem(item).subscribe(() => {
      this.loadShoppingList();
    });
  }

  moveToPreviouslyBought(item: ShoppingListItem) {
    this.shoppingListService.markAsBought(item).subscribe(() => {
      this.loadShoppingList();
    });
  }

  moveToToBuy(item: ShoppingListItem) {
    this.shoppingListService.markAsNotBought(item).subscribe(() => {
      this.loadShoppingList();
    });
  }

  copyToBuy(item: ShoppingListItem) {
    this.shoppingListService.copyToBuyListItem(item).subscribe(() => {
      this.loadShoppingList();
    });
  }
  delete(item: ShoppingListItem) {
    this.shoppingListService.deleteItem(item).subscribe(() => {
      this.loadShoppingList();
    });
  }
  private sortItemsByImportance() {
    const sortByImportanceAndName = (a: ShoppingListItem, b: ShoppingListItem) => {
      if (a.isImportant && !b.isImportant) {
        return -1; // a is important, b is not important, so a comes first
      } else if (!a.isImportant && b.isImportant) {
        return 1; // b is important, a is not important, so b comes first
      } else {
        // If both items have the same importance, sort alphabetically
        return a.name.localeCompare(b.name);
      }
    };

    this.shoppingList.sort(sortByImportanceAndName);
    this.previouslyBoughtList.sort(sortByImportanceAndName);
  }
}

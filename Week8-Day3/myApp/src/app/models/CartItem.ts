export class CartItem {
  Id: number;
  Count: number;

  constructor(id: number, count: number = 1) {
    this.Id = id;
    this.Count = count;
  }
}
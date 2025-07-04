export class ProductModel{
    constructor(
        public id: number, 
        public title: string,
        public description: string, 
        public price: number,
        public thumbnail: string
    )
    {}
}
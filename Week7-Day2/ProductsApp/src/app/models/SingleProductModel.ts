export class SingleProductModel
{
    constructor(public id:number=0,
        public title:string="",
        public price:number=0,
        public thumbnail:string="",
        public discountPercentage:string="",
        public rating:string="", 
        public stock:string="",
        public brand:string="",
        public tags:string[]=[],
        public description:string="",
    ){

    }
}
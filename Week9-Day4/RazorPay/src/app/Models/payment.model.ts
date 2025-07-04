export class PaymentModel{
    constructor(
        public  name: string,
        public email: string,
        public contactno: string,
        public amount: number,
        public productName: string
    )
    {}
}
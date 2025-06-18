export class FilterModel {
  constructor(
    public gender: string = '',
    public age: number | null = null,
    public role: string = ''
  ) {}
}
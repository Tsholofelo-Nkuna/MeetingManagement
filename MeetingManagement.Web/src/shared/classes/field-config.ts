export class FieldConfig<TValue, TModel> {
  controlType: 'input' | 'select' = 'input';
  label: string = '';
  value?: TValue;
  key: keyof TModel | '' = '';
}

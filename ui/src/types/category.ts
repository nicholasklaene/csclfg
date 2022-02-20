export interface Category {
  label: string;
}

export interface CategoryStoreState {
  loading: boolean;
  categories: Category[];
}

export const InitialCategoryStoreState: CategoryStoreState = {
  loading: false,
  categories: [],
};

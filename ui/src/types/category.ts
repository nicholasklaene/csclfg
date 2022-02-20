export interface Category {
  label: string;
}

export interface CategoryStoreState {
  loading: boolean;
  categories: Category[];
  default: string;
}

export const InitialCategoryStoreState: CategoryStoreState = {
  loading: false,
  categories: [],
  default: "Algorithms",
};

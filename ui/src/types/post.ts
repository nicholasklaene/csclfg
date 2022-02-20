import { SearchCriteria } from "./search";

export interface Post {
  post_id: string;
  category: string;
  created_at: number;
  description: string;
  title: string;
  tags: string[];
}

export interface PostStoreState {
  loading: boolean;
  posts: Post[];
  search: SearchCriteria;
}

export const InitialPostStoreState: PostStoreState = {
  loading: false,
  posts: [],
  search: {
    category: "Algorithms",
  },
};

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
  page: number;
  posts: Post[];
  search: SearchCriteria;
}

export const InitialPostStoreState: PostStoreState = {
  page: 1,
  posts: [],
  search: {
    category: "Algorithms",
  },
};

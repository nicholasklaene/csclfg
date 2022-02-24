import { SearchCriteria } from "./search";
import { defaultCategory, defaultSearchTimeSpanHours } from "../config";

export interface Post {
  post_id: string;
  category: string;
  created_at: number;
  description: string;
  title: string;
  tags: string[];
}

export interface CreatePost {
  title: string;
  category: string;
  description: string;
  tags: string[];
}

export interface PostStoreState {
  loading: boolean;
  posts: Post[];
  search: SearchCriteria;
  reachedEnd: boolean;
}

export const InitialPostStoreState: PostStoreState = {
  loading: false,
  reachedEnd: false,
  posts: [],
  search: {
    end: defaultSearchTimeSpanHours,
    category: defaultCategory,
  },
};

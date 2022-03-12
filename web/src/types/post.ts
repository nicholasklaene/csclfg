export interface Post {
  post_id: string;
  category: string;
  created_at: number;
  preview: string;
  description: string;
  title: string;
  tags: string[];
}

export interface CreatePost {
  title: string;
  category: string;
  preview: string;
  description: string;
  tags: string[];
}

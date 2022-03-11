export interface Search {
  start?: number;
  end: number;
  category: string;
  limit?: number;
  user?: string;
  tags?: string[];
}

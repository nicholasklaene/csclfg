export interface Search {
  start?: number;
  end: number;
  category: string;
  limit?: number;
  user?: string;
  tags?: string[];
}

export function getDefaultSearch(): Search {
  return {
    end: -1,
    category: "Algorithms",
  };
}

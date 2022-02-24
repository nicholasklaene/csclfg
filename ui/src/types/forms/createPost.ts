import { reactive } from "vue";
import { FormField } from ".";

export interface CreatePostForm {
  title: FormField<string>;
  description: FormField<string>;
  category: FormField<string>;
  tags: FormField<string[]>;
}

export const CreatePostFormInitialValue = () =>
  reactive({
    title: {
      value: "",
      error: false,
    },
    category: {
      value: "-1",
      error: false,
    },
    description: {
      value: "",
      error: false,
    },
    tags: {
      value: [] as string[],
      error: false,
    },
  });

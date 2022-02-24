export const validate = (formData: any): boolean => {
  let isValid = true;

  if (formData.title.value.length < 1 || formData.title.value.length > 100) {
    isValid = false;
    formData.title.error = true;
  } else {
    formData.title.error = false;
  }

  if (formData.category.value === "-1") {
    isValid = false;
    formData.category.error = true;
  } else {
    formData.category.error = false;
  }

  if (
    formData.description.value.length < 1 ||
    formData.description.value.length > 1024
  ) {
    isValid = false;
    formData.description.error = true;
  } else {
    formData.description.error = false;
  }

  return isValid;
};

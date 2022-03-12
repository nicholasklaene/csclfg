<script setup lang="ts">
import { ref, onMounted } from "vue";
import Editor from "@toast-ui/editor";
const editorDiv = ref<HTMLElement | null>();

let editor: Editor;
onMounted(() => {
  editor = new Editor({
    el: editorDiv.value!,
    height: "500px",
    initialEditType: "wysiwyg",
    previewStyle: "vertical",
    theme: "dark",
  });
});
</script>

<template>
  <div ref="editorDiv"></div>
</template>

<style>
.toastui-editor-defaultUI-toolbar,
.toastui-editor-dark .toastui-editor-md-container,
.toastui-editor-dark .toastui-editor-ww-container,
.toastui-editor-dark .toastui-editor-mode-switch,
.toastui-editor-dark .toastui-editor-mode-switch .tab-item {
  background-color: #111827 !important;
}
</style>

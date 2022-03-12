<script setup lang="ts">
import { ref, onMounted, defineEmits, defineProps } from "vue";
import Editor from "@toast-ui/editor";

const emit = defineEmits(["input"]);
const props = defineProps<{ value?: string }>();

const editorDiv = ref<HTMLFormElement | null>();

let editor: Editor;
onMounted(() => {
  editor = new Editor({
    el: editorDiv.value!,
    height: "420px",
    initialEditType: "wysiwyg",
    previewStyle: "vertical",
    theme: "dark",
    initialValue: props.value!,
  });
  addEvents();
});

function addEvents() {
  editor.addHook("keyup", () => {
    emit("input", editor.getMarkdown());
  });
}
</script>

<template>
  <div ref="editorDiv" class="mw-100"></div>
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

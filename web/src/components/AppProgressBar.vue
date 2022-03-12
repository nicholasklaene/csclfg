<template>
  <div class="progressBar">
    <div class="progressBar-value bg-primary"></div>
  </div>
</template>

<style scoped>
.progressBar {
  justify-content: flex-start;
  align-items: center;
  display: flex;
  height: 10px;
  width: 90%;
}
.progressBar-value {
  animation: load 5s normal forwards ease-out;
  height: 100%;
  width: 0;
}
@keyframes load {
  0% {
    width: 0;
  }
  60% {
    width: 85%;
  }
  100% {
    width: 100%;
  }
}
</style>

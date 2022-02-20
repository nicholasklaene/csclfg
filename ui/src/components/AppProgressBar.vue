<template>
  <div class="progress">
    <div class="progress-value bg-gray-200"></div>
  </div>
</template>

<style scoped>
.progress {
  justify-content: flex-start;
  align-items: center;
  display: flex;
  height: 10px;
  width: 100%;
}

.progress-value {
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
    width: 95%;
  }
}
</style>

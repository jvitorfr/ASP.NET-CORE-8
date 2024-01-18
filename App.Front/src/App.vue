<script setup lang="ts">
import HelloWorld from './components/HelloWorld.vue'
</script>

<template>
  <div class="container">
    <div class="row">
      <div class="col-md-4 col-md-offset-4 text-center">

        <header>
          <img alt="Vue logo" class="logo" src="./assets/logo.svg" width="125" height="125" />
        </header>

        <main>
          <HelloWorld />
        </main>
      </div>
    </div>
  </div>
</template>

<style scoped>
.logo {
  margin: 0 auto 2rem;
}
</style>

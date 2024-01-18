<template>
  <div class="container">
    <table class="table table-striped">
      <thead>
        <tr>
          <th>File Name</th>
          <th>Created At</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in items" :key="item.id">
          <td>{{ item.id }}</td>
          <td>{{ item.name }}</td>
          <td>{{ item.createdAt }}</td>
          <td>
            <div>
            <button class="btn btn-primary mr-2"> Download </button>
            <button class="btn btn-danger"> Exclude </button>
          </div>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script lang="ts">
import { Prop, Component, Vue, toNative } from 'vue-facing-decorator'

@Component
class TableComponent extends Vue {
  @Prop({})
  public items?: Array<{ id: string; name: string, createdAt: string }>;
  mounted() {
    console.log(this.items);
  }
}



export default toNative(TableComponent);
</script>
  
<style scoped>
/* You can include Bootstrap styles here or in your global styles */
</style>
  
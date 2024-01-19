<template>
  <div class="container">
    <table class="table table-striped">
      <thead>
        <tr>
          <th v-for="header in headers" :key="header">{{ header }}</th>
          <th v-if="hasActions">Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in items" :key="item.id">
          <td v-for="(value, index) in getObjectValues(item)" :key="index">
            {{ value ?? 'N/A' }}
          </td>
          <td v-if="hasActions">
            <div>
              <button v-for="(action, index) in actions" :key="index" @click="performAction(item, action)" class="mr-2 btn" :class="action.btn">
                <i class="bi" :class="action.icon"></i>
              </button>
            </div>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script lang="ts">
import { Prop, Component, Vue, toNative } from 'vue-facing-decorator';

interface Item {
  id: string;
  name: string;
  // Add other fields as needed
}

interface Action {
  icon: string;
  btn: string;
  perform: (item: Item) => void;
}

@Component
class TableComponent<T extends Item> extends Vue {
  @Prop({ type: Array as () => T[], required: true })
  public items!: T[];

  @Prop({ type: Array as () => Action[], required: true })
  public actions!: Action[];

  @Prop({ type: Array<string>, required: true })
  headers: string[] = [];

  mounted() {
    console.log(this.items);
    if(this.items == null) {
      return;
    }
    
  }

  get hasActions(): boolean {
    return this.actions && this.actions.length > 0;
  }

   getObjectValues(obj: Record<string, any>): any[] {
    return Object.values(obj);
  }

  performAction(item: Item, action: Action): void {
    action.perform(item);
  }
}

export default toNative(TableComponent);
</script>

<style scoped>
/* You can include Bootstrap styles here or in your global styles */
</style>

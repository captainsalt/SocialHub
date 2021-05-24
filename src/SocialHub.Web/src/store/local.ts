import PostModel from "@/models/PostModel";
import { computed, ref } from "vue";

export function usePostsStore() {
  const backingStore = ref<PostModel[]>([]);
  const posts = computed(() => backingStore.value);
  const setValue = (val: PostModel[]) => backingStore.value = val;

  return {
    posts,
    setValue
  };
}

<template>
  <div @dragover.prevent @drop.prevent>
    <input type="file" multiple @change="uploadFile" />
    <div @drop="dragFile">
      Or drag the file here
      <div v-if="files.length">
        <ul v-for="file in files" :key="file">
          {{
            file.name
          }}
        </ul>
      </div>
    </div>
  </div>
</template>

<script setup>
const files = ref([]);

function uploadFile(e) {
  files.value = e.target.files;
}

function dragFile(e) {
  files.value = e.dataTransfer.files;
}
</script>

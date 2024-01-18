<template>
    <div class="import-component">
        <TableComponent :items="items" />
        <input type="file" accept="" id="upload" ref="file" @change="handleFileChange" style="display:none;" />
        <button class="btn btn-primary" @click="click()">Upload</button>
        <button class="btn btn-default" @click="downloadFile()">Download</button>
    </div>
</template>

<script lang="ts">
import { defineComponent, type InputHTMLAttributes } from 'vue';
import TableComponent from "@/components/Table/Table.vue";

type FileResponse = {
    data: string,
    message: string
}[];

interface Data {
    loading: boolean,
    post: FileResponse | null,
    files : FileResponse | null
}

export default defineComponent({
    components: {
        TableComponent
    },
    computed: {
        uploadButton(): HTMLElement | null {
            return document.getElementById('upload');
        },
        items(): [] {
            return this.files?.data ?? [];
        }
    },
    data(): Data {
        return {
            loading: false,
            post: null,
            files: null,
        };
    },
    created() {
        this.fetchData();
    },
    watch: {
        '$route': 'fetchData'
    },

    methods: {
        click(): void {
            this.uploadButton?.click();
        },
        handleFileChange(event: any): void {
            const fileInput = this.uploadButton as any;
            const formData = new FormData();

            formData.append('file', fileInput?.files[0]);

            fetch('api/Import', {
                method: 'POST',
                body: formData,
            })
                .then(response => {
                    if (!response.ok) {
                        throw new Error(`HTTP error! Status: ${response.status}`);
                    }
                    return response.json();
                })
                .then(json => {
                    this.post = json as FileResponse;
                    this.loading = false;
                })
                .catch(error => {
                    console.error('Error:', error);
                });
        },
        async downloadFile(): Promise<void> {
            if (this.post == null) {
                alert("Arquivo não upado.");
            }
            const fileUrl: string = '/api/Export/Download?id=' + this.post.data;

            try {
                const response = await fetch(fileUrl);

                if (!response.ok) {
                  alert("ocorreu um erro ao baixar os arquivo");
                }
                const contentDisposition = response.headers.get('content-disposition');
                    const fileNameMatch = contentDisposition && contentDisposition.match(/filename="(.+)"/);
                    const fileName = fileNameMatch ? fileNameMatch[1] : 'downloaded-file';

                    const blob = await response.blob();

                    const link = document.createElement('a');
                    link.href = window.URL.createObjectURL(blob);
                    link.download = fileName;
                    link.click();

            } catch (error) {
                alert("ocorreu um erro ao baixar os arquivo");
                console.error(error);
            }
        },

        fetchData(): void {
            this.loading = true;

              fetch('/api/Export/Files')
                 .then(r => r.json())
                 .then(json => {
                     this.files = json as FileResponse;
                     this.loading = false;        
                 });
          
        }
    },
});
</script>

<style scoped>
th {
    font-weight: bold;
}

tr:nth-child(even) {
    background: #F2F2F2;
}

tr:nth-child(odd) {
    background: #FFF;
}

th,
td {
    padding-left: .5rem;
    padding-right: .5rem;
}

.weather-component {
    text-align: center;
}

table {
    margin-left: auto;
    margin-right: auto;
}
</style>
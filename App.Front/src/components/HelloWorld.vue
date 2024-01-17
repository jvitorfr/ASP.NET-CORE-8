<template>
    <div class="import-component">
    <input type="file" accept="" id="upload" ref="file"  @change="handleFileChange" style="display:none;"   /> 
        <button class="btn btn-primary" @click="click()" >Upload</button>
    </div>
</template>

<script lang="ts">
    import { defineComponent, type InputHTMLAttributes } from 'vue';

    type Forecasts = {
        date: string,
        temperatureC: string,
        temperatureF: string,
        summary: string
    }[];

    interface Data {
        loading: boolean,
        post: null | Forecasts
    }

    export default defineComponent({
        computed: {
            uploadButton(): HTMLElement | null {
                return document.getElementById('upload');
            }
        },
        data(): Data {
            return {
                loading: false,
                post: null
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
            handleFileChange(event: any) {
                const fileInput = this.uploadButton;
                const formData = new FormData();

                formData.append('file', fileInput?.files[0]);

                fetch('api/Import', {
                    method: 'POST',
                    body: formData,
                })
                    .then(response => {
                        console.log(response);
                        if (!response.ok) {
                            throw new Error(`HTTP error! Status: ${response.status}`);
                        }
                        return response.json();
                    })
                    .then(json => {
                        this.post = json as Forecasts;
                        this.loading = false;
                    })
                    .catch(error => {
                        console.error('Error:', error);
                    });


            },
            fetchData(): void {
                this.post = null;
                this.loading = true;

               /*  fetch('weatherforecast')
                    .then(r => r.json())
                    .then(json => {
                        this.post = json as Forecasts;
                        this.loading = false;
                        return;
                    });
              */
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

th, td {
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
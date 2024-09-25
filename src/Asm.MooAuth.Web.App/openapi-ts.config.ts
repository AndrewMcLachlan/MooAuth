/*import { createClient } from '@hey-api/openapi-ts';

createClient({
  client: '@hey-api/client-fetch',
  input: 'http://localhost:5112/openapi/v1.json',
  output: 'src/client',
});*/


import { defineConfig } from '@hey-api/openapi-ts';

export default defineConfig({
  client: '@hey-api/client-fetch',
  input: 'http://localhost:5112/openapi/v1.json',
  output: 'src/client'
});

# Frontend Development Guidelines

## Build Verification

**IMPORTANT:** After making changes to frontend code, always verify by running:

```bash
npm run generate  # Regenerate API types from backend (requires backend running)
npm run build     # Build and type-check the frontend
```

Do NOT assume changes work based solely on TypeScript IntelliSense or `tsc --noEmit`. The full build process may catch errors that incremental checks miss.

## API Code Generation

The `src/api/` directory contains auto-generated code from the backend OpenAPI spec. **Never edit these files manually.**

### Regenerating API Types

1. Start the backend API:
   ```bash
   cd ../Asm.MooAuth.Web.Api
   dotnet run
   ```

2. Generate TypeScript types:
   ```bash
   npm run generate
   ```

This fetches the OpenAPI spec from `http://localhost:5006/openapi/v1.json` and generates:
- `types.gen.ts` - TypeScript interfaces for all API models
- `sdk.gen.ts` - API client functions
- `@tanstack/react-query.gen.ts` - React Query hooks and options

### Naming Conventions

The openapi-ts generator uses lowercase for multi-word route segments:
- Route `/datasources/freetext` generates `getFreetextDataSource` (not `getFreeTextDataSource`)
- Route `/datasources/staticlist` generates `getStaticlistDataSource` (not `getStaticListDataSource`)
- Route `/datasources/apilist` generates `getApilistDataSource` (not `getApiListDataSource`)

Always check the generated code for exact function names before writing hooks.

## Hooks Pattern

Page-level hooks wrap the generated React Query functions. Located in `src/pages/<feature>/hooks/`.

Example hook:
```typescript
import { useQuery } from "@tanstack/react-query";
import { getFreetextDataSourceOptions } from "../../../api/@tanstack/react-query.gen";

export const useGetFreeTextDataSource = (id: number) => {
    return useQuery({
        ...getFreetextDataSourceOptions({ path: { id } }),
        enabled: id !== undefined,
    });
};
```

## Form Components

Use components from `@andrewmclachlan/moo-ds`:
- `Form.Group`, `Form.Label`, `Form.Input`, `Form.TextArea`, `Form.Select`, `Form.Password`
- `SectionForm` for form sections with react-hook-form integration

Note: `Form.Text` does not exist. Use `placeholder` attribute for hints instead.

## Path Parameters

Check the generated `types.gen.ts` for exact path parameter names. The backend may use different names than expected:
- `valueId` instead of `id` for nested resources
- `dataSourceId` instead of `id` for parent references

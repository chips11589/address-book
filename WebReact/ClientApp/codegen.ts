import { CodegenConfig } from '@graphql-codegen/cli'

const config: CodegenConfig = {
    schema: 'http://localhost:30232/graphql/',
    documents: './src/**/*.graphql',
    ignoreNoDocuments: true, // for better experience with the watcher
    generates: {
        './src/graphql/graphql-operations.ts': {
            plugins: ['typescript', 'typescript-operations', 'typed-document-node']
        }
    }
}

export default config
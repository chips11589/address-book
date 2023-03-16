import { GraphQLClient } from 'graphql-request';
import { GetContactsWithTagsDocument } from '../graphql/graphql-operations';

function ContactService() {
    console.log('ContactService initialized');

    const client = new GraphQLClient('http://localhost:30232/graphql/', {
        fetch
    });

    return {
        getContacts: (searchQuery) => {
            return client.request(GetContactsWithTagsDocument, {
                searchQuery: searchQuery
            });
        }
    };
}

const contactService = ContactService();

export default contactService;
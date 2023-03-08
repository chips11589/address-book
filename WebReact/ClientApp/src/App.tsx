import { GraphQLClient } from 'graphql-request';
import * as React from 'react';
import { Route, Routes } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import { Layout } from './components/Layout';
import './custom.css';
import { GetContactsWithTagsDocument } from './graphql/graphql-operations';

const client = new GraphQLClient('http://localhost:30232/graphql/', {
    fetch
});

export default class App extends React.Component {
    static displayName = App.name;

    constructor(props) {
        super(props);

        client.request(GetContactsWithTagsDocument, {
            searchQuery: 'sample'
        }).then(data => console.log(data.contacts));
    }

    render() {
        return (
            <Layout>
                <Routes>
                    {AppRoutes.map((route, index) => {
                        const { element, ...rest } = route;
                        return <Route key={index} {...rest} element={element} />;
                    })}
                </Routes>
            </Layout>
        );
    }
}

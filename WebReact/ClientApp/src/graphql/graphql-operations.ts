import { TypedDocumentNode as DocumentNode } from '@graphql-typed-document-node/core';
export type Maybe<T> = T | null;
export type InputMaybe<T> = Maybe<T>;
export type Exact<T extends { [key: string]: unknown }> = { [K in keyof T]: T[K] };
export type MakeOptional<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]?: Maybe<T[SubKey]> };
export type MakeMaybe<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]: Maybe<T[SubKey]> };
/** All built-in and custom scalars, mapped to their actual values */
export type Scalars = {
  ID: string;
  String: string;
  Boolean: boolean;
  Int: number;
  Float: number;
  UUID: any;
};

export type ContactDto = {
  __typename?: 'ContactDto';
  companyName?: Maybe<Scalars['String']>;
  email?: Maybe<Scalars['String']>;
  firstName?: Maybe<Scalars['String']>;
  id: Scalars['UUID'];
  linkedIn?: Maybe<Scalars['String']>;
  phone?: Maybe<Scalars['String']>;
  skype?: Maybe<Scalars['String']>;
  surname?: Maybe<Scalars['String']>;
  tagIds?: Maybe<Array<Scalars['UUID']>>;
  tags?: Maybe<Array<Maybe<TagDto>>>;
  title?: Maybe<Scalars['String']>;
};

export type GetContactsQueryInput = {
  searchQuery?: InputMaybe<Scalars['String']>;
  tagId?: InputMaybe<Scalars['UUID']>;
};

export type Query = {
  __typename?: 'Query';
  contacts?: Maybe<Array<Maybe<ContactDto>>>;
  tags?: Maybe<Array<Maybe<TagDto>>>;
};


export type QueryContactsArgs = {
  query?: InputMaybe<GetContactsQueryInput>;
};

export type TagDto = {
  __typename?: 'TagDto';
  id: Scalars['UUID'];
  name?: Maybe<Scalars['String']>;
};

export type GetContactsWithTagsQueryVariables = Exact<{
  searchQuery: Scalars['String'];
}>;


export type GetContactsWithTagsQuery = { __typename?: 'Query', contacts?: Array<{ __typename?: 'ContactDto', id: any, firstName?: string | null, tags?: Array<{ __typename?: 'TagDto', id: any, name?: string | null } | null> | null } | null> | null };


export const GetContactsWithTagsDocument = {"kind":"Document","definitions":[{"kind":"OperationDefinition","operation":"query","name":{"kind":"Name","value":"GetContactsWithTags"},"variableDefinitions":[{"kind":"VariableDefinition","variable":{"kind":"Variable","name":{"kind":"Name","value":"searchQuery"}},"type":{"kind":"NonNullType","type":{"kind":"NamedType","name":{"kind":"Name","value":"String"}}}}],"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"contacts"},"arguments":[{"kind":"Argument","name":{"kind":"Name","value":"query"},"value":{"kind":"ObjectValue","fields":[{"kind":"ObjectField","name":{"kind":"Name","value":"searchQuery"},"value":{"kind":"Variable","name":{"kind":"Name","value":"searchQuery"}}}]}}],"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"id"}},{"kind":"Field","name":{"kind":"Name","value":"firstName"}},{"kind":"Field","name":{"kind":"Name","value":"tags"},"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"id"}},{"kind":"Field","name":{"kind":"Name","value":"name"}}]}}]}}]}}]} as unknown as DocumentNode<GetContactsWithTagsQuery, GetContactsWithTagsQueryVariables>;
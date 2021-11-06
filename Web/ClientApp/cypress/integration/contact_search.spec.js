/// <reference types="cypress" />

describe('address-book app tests', () => {
    beforeEach(() => {
        cy.visit('https://localhost:5001/contact')
    })

    it('search contacts by company name', () => {
        cy.get('header contact-search').type(`sam{enter}`)
            .find('.tt-suggestion:first').click();
    })
})

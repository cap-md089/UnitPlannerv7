// modal.spec.js: in depth tests for modals
//
// Copyright (C) 2022 Andrew Rioux
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

describe('modal dialogue', () => {
    before(() => {
        Cypress.config('baseUrl', 'http://reactor.localunitplanner.org')
    });

    it('should open the modal', () => {
        cy.visit('/src/Integration/Components/Modal/ModalOpening.elm');

        cy.get('.button').first().click();

        cy.wait(100);

        cy
            .get('.modal-background')
            .should('have.css', 'background-color')
            .then(alpha => +alpha.replace('rgba(0, 0, 0, ','').replace(')', ''))
            .should('be.closeTo', 0.15, 0.149);

        cy.wait(300);

        cy.get('.modal-background').should('have.css', 'background-color', 'rgba(0, 0, 0, 0.3)')

        cy.contains('Modal text');
    });

    it('should close the modal after opening it by clicking the button', () => {
        cy.visit('/src/Integration/Components/Modal/ModalOpening.elm');

        cy.get('.button').first().click();

        cy.wait(400);

        cy.contains('Close modal').click();

        cy.wait(400);

        cy.contains('Close modal').should('not.exist');
    });

    it('should close the modal by clicking on the background', () => {
        cy.visit('/src/Integration/Components/Modal/ModalOpening.elm');

        cy.get('.button').first().click();

        cy.wait(400);

        cy.get('.modal-background').click(40, 40);

        cy.wait(400);

        cy.contains('Close modal').should('not.exist');
    });

    it('should not close the modal by clicking on the modal', () => {
        cy.visit('/src/Integration/Components/Modal/ModalOpening.elm');

        cy.get('.button').first().click();

        cy.wait(400);

        cy.contains('Modal text').click();

        cy.wait(400);

        cy.contains('Close modal');
    })
});
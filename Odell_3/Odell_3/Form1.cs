// Programmer: Meredith Odell
// Date: 11/03/2019
// Project: Odell_3
// Description: Meredith Odell Individual Assignment 3

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Odell_3
{
    public partial class Form1 : Form
    {
        // Declare and initialize class-level constants for tax rates
        private const decimal TAX_RATE = 0.07m;
        private const decimal DELIVERY_RATE = 7.50m;
        private const decimal SINGLE_BALLOON_RATE = 9.95m;
        private const decimal HALF_DOZEN_BALLOON_RATE = 35.95m;
        private const decimal DOZEN_BALLOON_RATE = 65.95m;
        private const decimal EXTRAS_RATE = 9.50m;
        private const decimal MESSAGE_RATE = 2.50m;

        public Form1()
        {
            InitializeComponent();
        }

        // Display the starting values and disable features unavailable at the start
        private void Form1_Load(object sender, EventArgs e)
        {
            // Declare and initialize local variables for use in calculation of totals
            decimal startingSubtotalPrice = SINGLE_BALLOON_RATE;
            decimal startingSalesTax = startingSubtotalPrice * TAX_RATE;
            decimal startingOrderTotal = startingSubtotalPrice + startingSalesTax;

            // Displays starting prices
            subtotalLabel.Text = startingSubtotalPrice.ToString("c");
            taxLabel.Text = startingSalesTax.ToString("c");
            totalLabel.Text = startingOrderTotal.ToString("c");

            // Displays the selected radio buttons for the default settings
            pickupRadioButton.Checked = true;
            singleRadioButton.Checked = true;

            // Initially has the check box unchecked, and the label and textbox disabled
            messageCheckBox.Checked = false;
            messageLabel.Enabled = false;
            messageTextBox.Enabled = false;

            // Call custom method to read in data from the two external txt files
            // and fill the listbox and combobox
            PopulateBoxes();

            // Sets default selection on the combobox as Birthday
            occasionComboBox.SelectedItem = "Birthday";
        }

        // Custom method to read data from external files
        private void PopulateBoxes()
        {
            try
            {
                StreamReader inputFile;
                
                // Assigns the Extras.txt file to the inputFile
                inputFile = File.OpenText("Extras.txt");

                while (!inputFile.EndOfStream)
                {
                    // Populates the listbox
                    extrasListBox.Items.Add(inputFile.ReadLine());
                }

                // Closes the Extras.txt file
                // Always close the file when you're done with it
                inputFile.Close();

                // Reassigns the occasions.txt file to the inputFile
                inputFile = File.OpenText("Occasions.txt");

                while (!inputFile.EndOfStream)
                {
                    // Populates the combobox
                    occasionComboBox.Items.Add(inputFile.ReadLine());
                }

                // Closes the Occasions.txt file
                inputFile.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Custom method to reset the form to it's orginal settings
        private void ResetForm()
        {
            // Declare and initialize local variables for use in calculation of totals
            decimal startingSubtotalPrice = SINGLE_BALLOON_RATE;
            decimal startingSalesTax = startingSubtotalPrice * TAX_RATE;
            decimal startingOrderTotal = startingSubtotalPrice + startingSalesTax;

            // Displays the default starting prices
            subtotalLabel.Text = startingSubtotalPrice.ToString("c");
            taxLabel.Text = startingSalesTax.ToString("c");
            totalLabel.Text = startingOrderTotal.ToString("c");

            titleComboBox.Text = "";
            firstNameTextBox.Text = "";
            lastNameTextBox.Text = "";
            streetTextBox.Text = "";
            cityTextBox.Text = "";
            stateMaskedTextBox.Text = "";
            zipCodeMaskedTextBox.Text = "";
            phoneNumberMaskedTextBox.Text = "";
            deliveryMaskedTextBox.Text = "";
            messageTextBox.Text = "";

            // Displays the default selections for the form
            pickupRadioButton.Checked = true;
            singleRadioButton.Checked = true;

            // Initially has the check box unchecked, and the label and textbox disabled
            messageCheckBox.Checked = false;
            messageLabel.Enabled = false;
            messageTextBox.Enabled = false;

            // Clear selected options and the imported strings 
            // in the extras listbox and occasions combobox
            extrasListBox.ClearSelected();
            occasionComboBox.SelectedItem = "Birthday";

            // Sends the focus to the title combobox
            titleComboBox.Focus();
        }

        // Handle the clear button's click event
        private void ClearButton_Click(object sender, EventArgs e)
        {
            // Call custom method to return form to it's original state
            ResetForm();
        }

        // Custom method to perform calculations for the total price
        private void UpdateTotals()
        {
            // Declare and initialize local variables
            decimal total;
            decimal salesTax;
            decimal subtotal = 0.00m;
            decimal extraNumber = extrasListBox.SelectedItems.Count;
            decimal extraTotal;

            // Handles calculations depending on the radio buttons selected
            if (singleRadioButton.Checked)
            {
                subtotal += SINGLE_BALLOON_RATE;
            }
            else if (halfDozenRadioButton.Checked)
            {
                subtotal += HALF_DOZEN_BALLOON_RATE;
            }
            else
            {
                subtotal += DOZEN_BALLOON_RATE;
            }

            if (deliveryRadioButton.Checked)
            {
                subtotal += DELIVERY_RATE;
            }

            if (messageCheckBox.Checked)
            {
                subtotal += MESSAGE_RATE;
            }

            extraTotal = extraNumber * EXTRAS_RATE;
            subtotal += extraTotal;
            salesTax = subtotal * TAX_RATE;

            total = salesTax + subtotal;

            // Dispplays the prices in their respective labels
            subtotalLabel.Text = subtotal.ToString("c");
            taxLabel.Text = salesTax.ToString("c");
            totalLabel.Text = total.ToString("c");
        }

        // Handle the exit button's click event
        // Displays a message asking the user if they really want to exit the program
        private void ExitButton_Click(object sender, EventArgs e)
        {
            // Displays a messagebox with a title, yes and no buttons and a warning icon
            DialogResult dialog = MessageBox.Show("Do you really want to exit the program?",
                "Exit Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            // If-Else statement to close program or return to first input object on form
            // If user presses "Yes" then the program will close
            if (dialog == DialogResult.Yes)
            {
                this.Close();
            }
            // If user presses "No" the program will not close and the focus will be sent 
            // to the first input on the form
            else if (dialog == DialogResult.No)
            {
                titleComboBox.Focus();
            }
        }

        // Handles the message checkbox's CheckedChange event
        private void MessageCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // If the message checkbox is checked, the message label and textbox will be enabled
            if (messageCheckBox.Checked)
            {
                messageLabel.Enabled = true;
                messageTextBox.Enabled = true;

                // Sends the user's focus to the message textbox
                messageTextBox.Focus();
            }
            // If the message checkbox is unchecked, the message label and textbox will be disabled
            else
            {
                messageLabel.Enabled = false;
                messageTextBox.Enabled = false;
            }

            // Call custom method to perform calculations
            UpdateTotals();
        }

        // Handle the message textbox's text changed event
        private void MessageTextBox_TextChanged(object sender, EventArgs e)
        {
            summaryButton.Focus();
        }

        // Handles the pickup radiobutton's CheckedChanged event
        private void PickupRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Call custom method to perform calculations
            UpdateTotals();
        }

        // Handles the single radiobutton's CheckedChange event
        private void SingleRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Call custom method to perform calculations
            UpdateTotals();
        }

        // Handles the half dozen radiobutton's Checkedchange event
        private void HalfDozenRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Call custom method to perform calculations
            UpdateTotals();
        }

        // Handles the occasion combobox's SelectedIndexChanged event
        private void OccasionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Call custom method to perform calculations
            UpdateTotals();
        }

        // Handles the extras listbox's SelectedIndexChanged event
        private void ExtrasListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Call custom method to perform calculations
            UpdateTotals();
        }

        // Handles the summary button's click event
        private void SummaryButton_Click(object sender, EventArgs e)
        {
            // Declare and initialize local variables
            string deliveryType;
            string bundleSize;
            string occasion = occasionComboBox.Text;

            // Handles which option to display for the radio button selections
            if (deliveryRadioButton.Checked)
            {
                deliveryType = "Home Delivery";
            }
            else
            {
                deliveryType = "Store Pickup";
            }

            if (singleRadioButton.Checked)
            {
                bundleSize = "Single";
            }
            else if (halfDozenRadioButton.Checked)
            {
                bundleSize = "Half Dozen";
            }
            else
            {
                bundleSize = "Dozen";
            }

            // Use StringBuilder to create the strings for the selected items in the listbox
            StringBuilder extras = new StringBuilder();
            foreach (object selectedItem in extrasListBox.SelectedItems)
            {
                extras.AppendLine(selectedItem.ToString());
            }
        
            // Displays an error message if the first name and last name are not filled, or if the phone number was incomplete
            if (firstNameTextBox.Text == "" || lastNameTextBox.Text == "" || phoneNumberMaskedTextBox.MaskCompleted == false)
            {
                MessageBox.Show("You must enter a first name, last name, and phone number to the order.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            // Displays the summary if all data is correct
            else
            { 
                MessageBox.Show("Bonnie's Balloons Order Summary" + Environment.NewLine
                    + "Customer Name: " + titleComboBox.Text + " " + firstNameTextBox.Text + " " + lastNameTextBox.Text + Environment.NewLine
                    + "Customer Address: " + streetTextBox.Text + ", " + cityTextBox.Text + ", " + stateMaskedTextBox.Text + ", " + zipCodeMaskedTextBox.Text + Environment.NewLine
                    + "Customer Phone Number: " + phoneNumberMaskedTextBox.Text + Environment.NewLine
                    + "Delivery Date: " + deliveryMaskedTextBox.Text + Environment.NewLine
                    + "Delivery Type: " + deliveryType + Environment.NewLine
                    + "Bundle Size: " + bundleSize + Environment.NewLine
                    + "Occasion: " + occasion + Environment.NewLine
                    + "Extras: " + Environment.NewLine
                    + extras 
                    + "Message: " + messageTextBox.Text + Environment.NewLine
                    + "Order Subtotal: " + subtotalLabel.Text + Environment.NewLine
                    + "Sales Tax Amount: " + taxLabel.Text + Environment.NewLine
                    + "Order Total: " + totalLabel.Text,
                    "Order Summary",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                
                // Call custom method to clear the form and send it back to it's orginal state
                ResetForm();
            }
        }
    }
}

        // Real-time password strength validation
        (function() {
            var passwordInput = document.getElementById('Password');
            var confirmInput = document.getElementById('ConfirmPassword');
            
            if (passwordInput) {
                passwordInput.addEventListener('input', function() {
                    var password = this.value;
                    
                    // Check length
                    var lengthReq = document.getElementById('lengthReq');
                    if (lengthReq) {
                        if (password.length >= 8) {
                            lengthReq.classList.add('valid');
                            lengthReq.classList.remove('invalid');
                        } else {
                            lengthReq.classList.add('invalid');
                            lengthReq.classList.remove('valid');
                        }
                    }
                    
                    // Check for number
                    var numberReq = document.getElementById('numberReq');
                    if (numberReq) {
                        if (/\d/.test(password)) {
                            numberReq.classList.add('valid');
                            numberReq.classList.remove('invalid');
                        } else {
                            numberReq.classList.add('invalid');
                            numberReq.classList.remove('valid');
                        }
                    }
                    
                    // Check for special character
                    var specialReq = document.getElementById('specialReq');
                    if (specialReq) {
                        var specialRegex = new RegExp('[!@%^&*(),.?{}|<>]');
                        if (specialRegex.test(password)) {
                            specialReq.classList.add('valid');
                            specialReq.classList.remove('invalid');
                        } else {
                            specialReq.classList.add('invalid');
                            specialReq.classList.remove('valid');
                        }
                    }
                });
            }
            
            if (passwordInput && confirmInput) {
                // Confirm password match
                confirmInput.addEventListener('input', function() {
                    if (this.value !== passwordInput.value) {
                        this.setCustomValidity('Passwords do not match');
                    } else {
                        this.setCustomValidity('');
                    }
                });
            }
            
            // Auto-hide alerts after 5 seconds
            setTimeout(function() {
                var alerts = document.querySelectorAll('.alert:not(.alert-info)');
                alerts.forEach(function(alert) {
                    alert.style.transition = 'opacity 0.5s';
                    alert.style.opacity = '0';
                    setTimeout(function() {
                        alert.style.display = 'none';
                    }, 500);
                });
            }, 5000);
        })();
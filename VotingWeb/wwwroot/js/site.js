
const vBtn = document.getElementById('newVoter');
const cBtn = document.getElementById('newCandidate');
vBtn.addEventListener('click', function () { showHide('newVoterForm', this) });
cBtn.addEventListener('click', function () { showHide('newCandidateForm', this) });

$('#ddlVoter').change(enableButton);
$('#ddlCandidate').change(enableButton);

function enableButton() {
    const sV = $('#ddlVoter').find(":selected").val();
    const sC = $('#ddlCandidate').find(":selected").val();
    const btn = $('#newVote');
    if (sV && sC) {
        btn.prop('disabled', false);
    } else {
        btn.prop('disabled', true);
    }
}

function showHide(id, btn) {
    const form = document.getElementById(id);

    if (form.style.display === 'none') {
        form.style.display = 'block';
        btn.innerText = '-'
    } else {
        form.style.display = 'none';
        btn.innerText = '+'
    }
}

$('#newVote').click(function () {
    const selectedVoter = $('#ddlVoter').find(":selected").val();
    const selectedCandidate = $('#ddlCandidate').find(":selected").val();
    $.ajax({
        url: "Home/Vote",
        type: "POST",
        data: { voterId: selectedVoter, candidateId: selectedCandidate },
        cache: false,
        async: true,
        success: function (result) {
            window.location.href = result.url
        }
    });
});